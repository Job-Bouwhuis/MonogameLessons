using Microsoft.Xna.Framework;
using SnowLibrary.Monogame;
using SnowLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using SnowLibrary.Serialization;
using SnowLibrary.Monogame.SceneManagement;
using SnowLibrary.Monogame.Debugging;
using Microsoft.Xna.Framework.Graphics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace CSharpAdvanced.Assignment2
{
    [IncludePrivateFields]
    public class Enemy : GameObject, IMyRenderable
    {
        public EnemyState state = EnemyState.Patrolling;

        private readonly List<Transform> patrolingPositions = new List<Transform>();
        [ExcludeFromSerialization]
        private int currentPatrolingTarget = 0;

        
        public float walkSpeed;

        public string TargetName = "Player1";
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

        }
        /// <summary>
        /// Adds all the positions to this enemy's patroling positions. the enemy will patrol between these positions. function will override all previous positions
        /// </summary>
        public void SetPatroling(params Transform[] positions)
        {
            patrolingPositions.Clear();
            positions.Foreach(x => patrolingPositions.Add(x));
        }

        public override void Awake()
        {
            var obj = SceneManager.CurrentScene.FindObjectWithName(TargetName);
            Type t = obj?.GetType();

            Player? p = obj as Player;

            target = obj;
        }

        public override void Update(GameTime time)
        {
            if (target == null)
                return;
                
            float distanceToTarget = Vector2.Distance(transform.position, target.transform.position);

            if (distanceToTarget < 200)
            {
                if (target is Player player)
                {
                    if (player.PlayerState == Player.State.WeaponShield)
                        state = EnemyState.Fleeing;
                    else
                        state = EnemyState.Chasing;
                }
            }

            if (state == EnemyState.Chasing)
            {
                MoveToTarget(target.transform, time);
                if (distanceToTarget > 250)
                    state = EnemyState.Patrolling;
            }

            if (state == EnemyState.Fleeing)
            {
                MoveToTarget(target.transform.position, time, true);
                if (distanceToTarget > 250)
                    state = EnemyState.Patrolling;
            }

            if (state == EnemyState.Patrolling)
            {
                timeWalked += time.ElapsedGameTime.TotalMilliseconds;
                if (timeWalked > 7000)
                {
                    state = EnemyState.Idle;
                    return;
                }


                MoveToTarget(patrolingPositions[currentPatrolingTarget], time);

                float targetDistance = Vector2.Distance(transform.position, patrolingPositions[currentPatrolingTarget]);
                if (targetDistance < 4)
                {
                    currentPatrolingTarget++;
                    
                    if (currentPatrolingTarget >= patrolingPositions.Count)
                        currentPatrolingTarget = 0;
                    
                    state = EnemyState.Idle;
                }
            }

            if (state == EnemyState.Idle)
            {
                if (timeIdle > 4000)
                {
                    state = EnemyState.Idle;
                    return;
                }

                timeIdle += time.ElapsedGameTime.TotalMilliseconds;
                if (timeIdle >= 4000)
                {
                    timeWalked = 0;
                    timeIdle = 0;
                    state = EnemyState.Patrolling;
                }
            }


        }

        private void MoveToTarget(Vector2 target, GameTime time, bool evade = false)
        {
            
            Vector2 directiont = evade ? Vector2.Normalize(transform.position - target) : Vector2.Normalize(target - transform.position);
            Vector2 direction = directiont * walkSpeed;
            
            transform.position += direction * (float)time.ElapsedGameTime.TotalMilliseconds;
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
    }

    public enum EnemyState
    {
        Idle,
        Patrolling,
        Chasing,
        Fleeing
    }
}
