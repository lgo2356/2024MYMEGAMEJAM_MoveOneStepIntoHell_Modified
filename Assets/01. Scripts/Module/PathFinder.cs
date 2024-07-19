using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TeamJustFour.MoveOneStep.Algorithm;
using UnityEngine;

namespace TeamJustFour.MoveOneStep.Module
{
    public class PathFinder : MonoBehaviour
    {
        [SerializeField] private GameObject m_PathPrefab;
        [SerializeField] private Transform m_Parent;

        private const int PIXEL_PER_UNIT_X = 96;
        private const int PIXEL_PER_UNIT_Y = 96;
        private const int WIDTH = 14;
        private const int HEIGHT = 8;

        public void StartFindPath(int startPosX, int startPosY, List<Vector2Int> walls, GameObject moverPrefab, float duration)
        {
            StartCoroutine(FindPathCoroutine(startPosX, startPosY, walls, moverPrefab, duration));
        }

        private IEnumerator FindPathCoroutine(int startPosX, int startPosY, List<Vector2Int> walls, GameObject moverPrefab, float duration)
        {
            yield return new WaitForSeconds(1f);

            Astar astar = new(WIDTH, HEIGHT);

            foreach (var wall in walls)
            {
                astar.SetWall(wall.x, wall.y);
            }

            List<Node> path = astar.FindPath(startPosX, startPosY, WIDTH - 1, HEIGHT - 1);

            float waitSeconds = duration / path.Count;
            int xPos = 960 - WIDTH * PIXEL_PER_UNIT_X / 2;
            int yPos = 540 - HEIGHT * PIXEL_PER_UNIT_Y / 2;

            foreach (var node in path)
            {
                GameObject tile = Instantiate(m_PathPrefab, m_Parent);
                tile.transform.position = new Vector2(node.X * 96 + xPos, (HEIGHT - node.Y - 1) * 96 + yPos);
            }

            GameObject moverInstance = Instantiate(moverPrefab, m_Parent);
            moverInstance.transform.position = new Vector2(path[0].X * 96 + xPos, (HEIGHT - path[0].Y - 1) * 96 + yPos);

            foreach (var node in path)
            {
                moverInstance.transform
                    .DOMove(new Vector2(node.X * 96 + xPos, (HEIGHT - node.Y - 1) * 96 + yPos), waitSeconds)
                    .SetEase(Ease.Linear);

                yield return new WaitForSeconds(waitSeconds);
            }
        }

        private void Start()
        {
            //StartFindPath(m_TilePrefab, 2f);

            //for (int i = 1; i < path.Count; i++)
            //{
            //    if (path[i].X > path[i - 1].X)
            //    {
            //        // 가로 이동
            //        Debug.Log("가로 이동");

            //        GameObject tile = Instantiate(m_TilePrefab, m_Parent);
            //        tile.transform.position = new Vector2(path[i].X * 96, path[i].Y * 96);
            //        tile.transform.Rotate(new(0, 0, 90f));
            //    }
            //    else if (path[i].Y > path[i - 1].Y)
            //    {
            //        // 세로 이동
            //        Debug.Log("세로 이동");

            //        GameObject tile = Instantiate(m_TilePrefab, m_Parent);
            //        tile.transform.position = new Vector2(path[i].X * 96, path[i].Y * 96);
            //    }
            //    else
            //    {
            //        GameObject tile = Instantiate(m_TilePrefab, m_Parent);
            //        tile.transform.position = new Vector2(path[i].X * 96, path[i].Y * 96);
            //        tile.transform.rotation = Quaternion.Euler(0, 0, 45f);
            //    }
            //}
        }
    }
}
