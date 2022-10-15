using Microsoft.Xna.Framework;
using WinterRose.Monogame;
using WinterRose;
using System;
using System.Collections.Generic;
using WinterRose.Serialization;
using WinterRose.Monogame.SceneManagement;
using Microsoft.Xna.Framework.Graphics;

namespace CSharpAdvanced.Assignment2
{
    [IncludePrivateFields]
    public class Enemy : GameObject, IMyRenderable
    {
        /// <summary>
        /// The name of the game object this enemy is targeting
        /// </summary>
        public string TargetName = "Player1";
        /// <summary>
        /// the speed of which this enemy will walk
        /// </summary>
        public float walkSpeed;
        /// <summary>
        /// The state of the enemy. This is used to determine what the enemy is doing
        /// </summary>
        public EnemyState State
        {
            get => state;
            set
            {
                lastState = state;
                state = value;
            }
        }

        private EnemyState lastState = EnemyState.Patrolling;
        
        private readonly List<Transform> patrolingPositions = new List<Transform>();
        
        [ExcludeFromSerialization]
        private int currentPatrolingTarget = 0;
        
        [ExcludeFromSerialization]
        private float distanceToTarget = 0;
        
        private EnemyState state;
        
        [ExcludeFromSerialization]
        private GameObject target;
        
        [ExcludeFromSerialization]
        private double timeWalked = 0, timeIdle = 0;

        public Enemy(string objectName, Vector2 position, params Sprite[] sprites) : base(objectName, new Transform(), sprites)
        {
            transform.position = position;
        }
        public Enemy() : base("new Enemy", new Transform())
        {
            SceneManager.OnNewSceneLoaded += AssignTarget;
        }
        
        /// <summary>
        /// Adds all the positions to this enemy's patroling positions. the enemy will patrol between these positions. function will override all previous positions
        /// </summary>
        public void SetPatrolingPoints(params Transform[] positions)
        {
            patrolingPositions.Clear();
            positions.Foreach(x => patrolingPositions.Add(x));
        }

        public void AssignTarget()
        {
            state = EnemyState.Patrolling;
            
            var obj = SceneManager.CurrentScene.FindGameObjectWithName(TargetName);

            target = obj;
        }

        public override void Update(GameTime time)
        {
            if (target == null)
                return;

            distanceToTarget = Vector2.Distance(transform.position, target.transform.position);

            if (distanceToTarget < 200 && target is Player player)
                if (player.PlayerState == Player.State.WeaponShield)
                    State = EnemyState.Fleeing;
                else
                    State = EnemyState.Chasing;

            switch (State)
            {
                case EnemyState.Idle:
                    Idling(time);
                    break;
                case EnemyState.Patrolling:
                    Patrolling(time);
                    break;
                case EnemyState.Chasing:
                    DifferFromPatrol(time, false);
                    break;
                case EnemyState.Fleeing:
                    DifferFromPatrol(time, true);
                    break;
            }
        }

        private void Patrolling(GameTime time)
        {
            if (lastState == EnemyState.Fleeing || lastState == EnemyState.Chasing)
            {
                FindClosestPatrolPoint();
                lastState = state;
            }

            if (timeWalked > 7000)
            {
                State = EnemyState.Idle;
                return;
            }
            timeWalked += time.ElapsedGameTime.TotalMilliseconds;
            MoveToTarget(patrolingPositions[currentPatrolingTarget], time);
            IdleWhenAtTarget();
        }

        private void FindClosestPatrolPoint()
        {
            List<float> distanceToTargets = new List<float>();

            patrolingPositions.Foreach(x => distanceToTargets.Add(Vector2.Distance(transform.position, x.position)));
            float smallest = distanceToTargets.Min();
            currentPatrolingTarget = distanceToTargets.IndexOf(smallest);
        }

        private void DifferFromPatrol(GameTime time, bool flee)
        {
            MoveToTarget(target.transform.position, time, flee);
            if (distanceToTarget > 250)
                State = EnemyState.Patrolling;
        }

        private void Idling(GameTime time)
        {
            // if enemy is idling for more than 4 seconds, change state to patrolling
            if (timeIdle < 4000)
            {
                timeIdle += time.ElapsedGameTime.TotalMilliseconds;
                return;
            }

            
            if (timeIdle >= 4000)
            {
                timeWalked = 0;
                timeIdle = 0;
                State = EnemyState.Patrolling;
            }
        }

        private void MoveToTarget(Vector2 target, GameTime time, bool evade = false)
        {
            // get a normalized direction vector from the enemy to the target, or when evading is true, the oposite direction than towards the player
            Vector2 directiont = evade ?
                Vector2.Normalize(transform.position - target)  : 
                Vector2.Normalize(target - transform.position);
            
            //apply movement
            transform.position += directiont * walkSpeed * (float)time.ElapsedGameTime.TotalMilliseconds;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Begin();
            //draw enemy
            batch.Draw(texture, transform, Color.White);

            //draw a flag on each position of the patroling positions
            textureIndex = 1;
            foreach (Vector2 patrolPosition in patrolingPositions)
                batch.Draw(texture, patrolPosition, Color.White);

            textureIndex = 0;
            batch.End();
        }

        public void IdleWhenAtTarget()
        {
            float targetDistance = Vector2.Distance(transform.position, patrolingPositions[currentPatrolingTarget]);
            if (targetDistance < 4)
            {
                currentPatrolingTarget++;

                if (currentPatrolingTarget >= patrolingPositions.Count)
                    currentPatrolingTarget = 0;

                State = EnemyState.Idle;
            }
        }
    }

    public enum EnemyState
    {
        Idle,
        Patrolling,
        Chasing,
        Fleeing
    }
}
