using System;
using System.Collections.Generic;
using System.Linq;
using Giroo.Utility;
using UnityEngine;

namespace _Scripts.Managers
{
    public class MovementManager : Singleton<MovementManager>
    {
        public List<(IMoveable, Vector2Int)> moveables = new List<(IMoveable, Vector2Int)>();

        private float _timer;

        public void AddMoveable(IMoveable moveable, Vector2Int direction)
        {
            moveables.Add((moveable, direction));
        }

        public void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= 0.1f)
            {
                _timer = 0f;

                if (!moveables.Any())
                    return;

                for (var index = moveables.Count - 1; index >= 0; index--)
                {
                    var moveable = moveables[index];
                    var path = AStar.FindPath(GridManager.Instance.grid.grid, moveable.Item1.GetCurrentPosition(), moveable.Item2);

                    if (path == null)
                    {
                        moveables.Remove(moveable);
                        continue;
                    }

                    moveable.Item1.Move(new Vector2Int(path[1][0], path[1][1]));

                    if (moveable.Item1.GetCurrentPosition() == moveable.Item2)
                    {
                        moveables.Remove(moveable);
                    }
                }
            }
        }
    }
}