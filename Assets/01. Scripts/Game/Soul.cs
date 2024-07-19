using System.Collections;
using TeamJustFour.MoveOneStep.Module;
using UnityEngine;

namespace TeamJustFour.MoveOneStep.Game
{
    public class Soul : MonoBehaviour
    {
        private const int PIXEL_PER_UNIT_X = 96;
        private const int PIXEL_PER_UNIT_Y = 96;
        private const int WIDTH = 14;
        private const int HEIGHT = 8;

        private Vector2 m_CurrentPosition;

        public void FindNextDirection(int[][] grid, int[][] blockGrid)
        {
            StartCoroutine(FindDirectionCoroutine(grid, blockGrid));

            //for (int i = 0; i < dx.Length; i++)
            //{
            //    int checkX = (int)m_CurrentPosition.x + dx[i];
            //    int checkY = (int)m_CurrentPosition.x + dy[i];

            //    if (checkX >= 0 && checkX < WIDTH && checkY >= 0 && checkY < HEIGHT)
            //    {
            //        //neighbours.Add(m_Grid[checkX, checkY]);
            //        if (m_CurrentPosition.x == checkX && m_CurrentPosition.y == checkY)
            //        {
            //            //m_CurrentPosition = new Vector2(checkX, checkY);
            //            SetPosition(checkX, checkY);
            //        }
            //    }
            //}

            //GameObject moverInstance = Instantiate(moverPrefab, m_Parent);
            //moverInstance.transform.position = new Vector2(path[0].X * 96 + xPos, (HEIGHT - path[0].Y - 1) * 96 + yPos);

            //foreach (var node in path)
            //{
            //    moverInstance.transform
            //        .DOMove(new Vector2(node.X * 96 + xPos, (HEIGHT - node.Y - 1) * 96 + yPos), waitSeconds)
            //        .SetEase(Ease.Linear);

            //    yield return new WaitForSeconds(waitSeconds);
            //}
        }

        public void SetPosition(int x, int y, bool immediately = false)
        {
            int xPos = 960 - WIDTH * PIXEL_PER_UNIT_X / 2;
            int yPos = 540 - HEIGHT * PIXEL_PER_UNIT_Y / 2;
            Vector3 vector = new(x * PIXEL_PER_UNIT_X + xPos, (HEIGHT - y - 1) * PIXEL_PER_UNIT_Y + yPos);

            transform.position = vector;

            m_CurrentPosition = new Vector2(x, y);
        }

        private IEnumerator FindDirectionCoroutine(int[][] pathGrid, int[][] blockGrid)
        {
            yield return new WaitForSeconds(2f);

            while (true)
            {
                int x = (int)m_CurrentPosition.x;
                int y = (int)m_CurrentPosition.y;

                if (blockGrid[y][x] == 2)
                {
                    GameClear();

                    break;
                }

                if (y > 0 && pathGrid[y - 1][x] == 3 || y > 0 && pathGrid[y - 1][x] == 2)
                {
                    if (CanMove(x, y - 1, blockGrid))
                    {
                        if (pathGrid[y - 1][x] != 2)
                        {
                            pathGrid[y - 1][x] = 0;
                        }

                        SetPosition(x, y - 1);
                    }
                    else
                    {
                        GameOver();
                    }
                }
                else if (y < HEIGHT - 1 && pathGrid[y + 1][x] == 3 || y < HEIGHT - 1 && pathGrid[y + 1][x] == 2)
                {
                    if (CanMove(x, y + 1, blockGrid))
                    {
                        if (y < HEIGHT - 1 && pathGrid[y + 1][x] != 2)
                        {
                            pathGrid[y + 1][x] = 0;
                        }

                        SetPosition(x, y + 1);
                    }
                    else
                    {
                        GameOver();
                    }
                }
                else if (x > 0 && pathGrid[y][x - 1] == 3 || x > 0 && pathGrid[y][x - 1] == 2)
                {
                    if (CanMove(x - 1, y, blockGrid))
                    {
                        if (x > 0 && pathGrid[y][x - 1] != 2)
                        {
                            pathGrid[y][x - 1] = 0;
                        }

                        SetPosition(x - 1, y);
                    }
                    else
                    {
                        GameOver();
                    }
                }
                else if (x < WIDTH - 1 && pathGrid[y][x + 1] == 3 || x < WIDTH - 1 && pathGrid[y][x + 1] == 2)
                {
                    if (CanMove(x + 1, y, blockGrid))
                    {
                        if (pathGrid[y][x + 1] != 2)
                        {
                            pathGrid[y][x + 1] = 0;
                        }

                        SetPosition(x + 1, y);
                    }
                    else
                    {
                        GameOver();
                    }

                    //transform.DOMove(new Vector2(m_CurrentPosition.x * 96 + xPos, (HEIGHT - m_CurrentPosition.y - 1) * 96 + yPos), 0.5f)
                    //    .SetEase(Ease.Linear)
                    //    .Play();
                }

                yield return new WaitForSeconds(1f);
            }
        }

        private bool CanMove(int x, int y, int[][] blockGrid)
        {
            if (blockGrid[y][x] == 8 || blockGrid[y][x] == 9 || blockGrid[y][x] == 10)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void GameOver()
        {
            InGameSceneGameManager.Instance.GameOver();
        }

        private void GameClear()
        {
            InGameSceneGameManager.Instance.GameClear();
        }
    }
}
