using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamJustFour.MoveOneStep.Game
{
    public class TilemapGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject m_TilePrefab;
        [SerializeField] private GameObject m_StartPrefab;
        [SerializeField] private GameObject m_EndPrefab;
        [SerializeField] private GameObject m_NPCPrefab;
        [SerializeField] private GameObject m_WallPrefab1;
        [SerializeField] private GameObject m_WallPrefab2;
        [SerializeField] private GameObject m_WallPrefab3;
        [SerializeField] private GameObject m_WallPrefab4;
        [SerializeField] private GameObject m_WallPrefab5;
        [SerializeField] private GameObject m_BlockPrefab1;
        [SerializeField] private GameObject m_BlockPrefab2;
        [SerializeField] private GameObject m_BlockPrefab3;
        [SerializeField] private Transform m_Parent;
        [SerializeField] private Transform m_Parent2;

        private const int PIXEL_PER_UNIT_X = 96;
        private const int PIXEL_PER_UNIT_Y = 96;
        private const int WIDTH = 14;
        private const int HEIGHT = 8;

        private int[][][] m_NPCPaths;
        private int[][][] m_StageBlocks;
        private Block[][] m_Blocks = new Block[HEIGHT][]
        {
            new Block[WIDTH] { null, null, null, null, null, null, null, null, null, null, null, null, null, null },
            new Block[WIDTH] { null, null, null, null, null, null, null, null, null, null, null, null, null, null },
            new Block[WIDTH] { null, null, null, null, null, null, null, null, null, null, null, null, null, null },
            new Block[WIDTH] { null, null, null, null, null, null, null, null, null, null, null, null, null, null },
            new Block[WIDTH] { null, null, null, null, null, null, null, null, null, null, null, null, null, null },
            new Block[WIDTH] { null, null, null, null, null, null, null, null, null, null, null, null, null, null },
            new Block[WIDTH] { null, null, null, null, null, null, null, null, null, null, null, null, null, null },
            new Block[WIDTH] { null, null, null, null, null, null, null, null, null, null, null, null, null, null },
        };

        /**
         * None : 0
         * Start : 1
         * End : 2
         * NPC : 3
         * Wall 1(¿ïÅ¸¸®) : 4
         * Wall 2(¹ÙÀ§) : 5
         * Wall 3(º®ºí·Ï) : 6
         * Wall 4(±¸¸Û) : 7
         * Wall 5(±¸¸Û2) : 11
         * Block 1(Áö¿Á¹ÙÀ§) : 8
         * Block 2(°¡½Ã¹Ù´Ú) : 9
         * Block 3(Áö¿Á¹Ù´Ú) : 10
         */
        public int[][] Stage1Blocks = new int[][]
        {
            new int[] { 7, 7, 4, 4, 8, 0, 0, 0, 8, 0, 4, 4, 7, 7 },
            new int[] { 4, 4, 4, 8, 8, 0, 11, 11, 11, 0, 0, 4, 4, 4 },
            new int[] { 0, 0, 0, 8, 0, 11, 7, 7, 7, 11, 8, 0, 0, 0 },
            new int[] { 1, 0, 8, 6, 0, 7, 0, 0, 0, 7, 0, 8, 0, 2 },
            new int[] { 0, 0, 0, 6, 0, 7, 0, 0, 0, 7, 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 0, 7, 7, 7, 7, 7, 8, 8, 0, 0 },
            new int[] { 4, 4, 4, 0, 0, 8, 7, 7, 7, 0, 0, 4, 4, 4 },
            new int[] { 11, 11, 4, 4, 0, 0, 0, 0, 8, 8, 4, 4, 11, 11 }
        };

        public int[][] Stage2Blocks = new int[][]
        {
            new int[] { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 },
            new int[] { 1, 0, 0, 0, 0, 0, 9, 9, 9, 9, 0, 0, 11, 11 },
            new int[] { 8, 0, 5, 0, 0, 0, 9, 9, 9, 0, 0, 0, 7, 7 },
            new int[] { 0, 8, 0, 0, 9, 9, 8, 0, 0, 6, 9, 0, 7, 7 },
            new int[] { 0, 0, 11, 11, 0, 0, 0, 6, 6, 0, 0, 0, 0, 0 },
            new int[] { 0, 8, 7, 7, 0, 0, 0, 0, 0, 0, 6, 8, 0, 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 6, 0, 9, 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 5, 4, 4, 4, 0, 8, 8, 2 }
        };

        public int[][] Stage3Blocks = new int[][]
        {
            new int[] { 7, 7, 0, 0, 0, 0, 6, 10, 0, 0, 0, 0, 6, 2 },
            new int[] { 7, 0, 0, 5, 0, 8, 8, 9, 8, 9, 10, 8, 6, 0 },
            new int[] { 0, 0, 8, 8, 11, 1, 11, 0, 6, 0, 8, 0, 6, 0 },
            new int[] { 0, 0, 0, 5, 7, 11, 7, 10, 6, 10, 0, 0, 9, 0 },
            new int[] { 4, 4, 9, 8, 0, 7, 8, 10, 6, 0, 0, 4, 4, 0 },
            new int[] { 0, 0, 9, 10, 0, 0, 0, 0, 8, 9, 10, 9, 8, 8 },
            new int[] { 8, 10, 5, 8, 0, 0, 0, 0, 5, 0, 0, 0, 0, 5 },
            new int[] { 0, 8, 0, 0, 10, 0, 0, 0, 8, 0, 0, 0, 0, 0 }
        };

        public int[][][] StageBlocks
        {
            get
            {
                if (m_StageBlocks == null)
                {
                    m_StageBlocks = new int[3][][];
                    m_StageBlocks[0] = Stage1Blocks;
                    m_StageBlocks[1] = Stage2Blocks;
                    m_StageBlocks[2] = Stage3Blocks;
                }

                return m_StageBlocks;
            }
        }

        public int[][] NPCPath1 = new int[][]
        {
            new int[] { 0, 0, 0, 0, 0, 3, 3, 3, 3, 3, 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 3, 3, 0, 0, 0, 3, 3, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 3, 0, 0, 0 },
            new int[] { 1, 3, 3, 0, 3, 0, 0, 0, 0, 0, 3, 3, 3, 2 },
            new int[] { 0, 0, 3, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[] { 0, 0, 3, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        };

        public int[][] NPCPath2 = new int[][]
        {
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[] { 1, 3, 0, 0, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0 },
            new int[] { 0, 3, 0, 0, 3, 0, 0, 0, 0, 0, 3, 0, 0, 0 },
            new int[] { 0, 3, 0, 0, 3, 3, 0, 0, 0, 0, 3, 0, 0, 0 },
            new int[] { 3, 3, 0, 0, 0, 3, 0, 0, 0, 0, 3, 3, 0, 0 },
            new int[] { 3, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 3, 0, 0 },
            new int[] { 3, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 3, 0, 0 },
            new int[] { 3, 3, 3, 3, 3, 3, 0, 0, 0, 0, 0, 3, 3, 2 }
        };

        public int[][] NPCPath3 = new int[][]
        {
            new int[] { 0, 0, 3, 3, 3, 0, 0, 3, 3, 3, 3, 3, 0, 2 },
            new int[] { 0, 3, 3, 0, 3, 3, 0, 3, 0, 0, 0, 3, 0, 3 },
            new int[] { 3, 3, 0, 0, 0, 1, 0, 3, 0, 0, 0, 3, 0, 3 },
            new int[] { 3, 3, 3, 0, 0, 0, 0, 3, 0, 0, 3, 3, 0, 3 },
            new int[] { 0, 0, 3, 0, 0, 0, 0, 3, 0, 0, 3, 0, 0, 3 },
            new int[] { 0, 3, 3, 0, 0, 0, 0, 3, 0, 0, 3, 0, 3, 3 },
            new int[] { 0, 3, 0, 0, 0, 3, 3, 3, 0, 0, 3, 3, 3, 0 },
            new int[] { 0, 3, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0 }
        };

        public int[][][] NPCPaths
        {
            get
            {
                if (m_NPCPaths == null)
                {
                    m_NPCPaths = new int[3][][];
                    m_NPCPaths[0] = NPCPath1;
                    m_NPCPaths[1] = NPCPath2;
                    m_NPCPaths[2] = NPCPath3;
                }

                return m_NPCPaths;
            }
        }

        // 0 ~ 2
        public int[][] GenerateTilemap(int stage)
        {
            GameObject tileMapParent = new("Tilemap");
            tileMapParent.transform.SetParent(m_Parent);

            int[][] stageBlocks = StageBlocks[stage];

            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    GameObject tile = Instantiate(m_TilePrefab, tileMapParent.transform);
                    tile.transform.position = new Vector2(x * PIXEL_PER_UNIT_X, (HEIGHT - y - 1) * PIXEL_PER_UNIT_Y);

                    switch (stageBlocks[y][x])
                    {
                        case 0:
                            break;

                        case 1:
                            {
                                tile = Instantiate(m_StartPrefab, tileMapParent.transform);
                                tile.transform.position = new Vector2(x * PIXEL_PER_UNIT_X, (HEIGHT - y - 1) * PIXEL_PER_UNIT_Y);
                            }
                            break;

                        case 2:
                            {
                                tile = Instantiate(m_EndPrefab, tileMapParent.transform);
                                tile.transform.position = new Vector2(x * PIXEL_PER_UNIT_X, (HEIGHT - y - 1) * PIXEL_PER_UNIT_Y);
                            }
                            break;

                        case 3: // NPC
                            {
                                tile = Instantiate(m_NPCPrefab, tileMapParent.transform);
                                tile.transform.position = new Vector2(x * PIXEL_PER_UNIT_X, (HEIGHT - y - 1) * PIXEL_PER_UNIT_Y);

                                m_Blocks[y][x] = tile.GetComponent<Block>();
                            }
                            break;

                        case 4:
                            {
                                tile = Instantiate(m_WallPrefab1, tileMapParent.transform);
                                tile.transform.position = new Vector2(x * PIXEL_PER_UNIT_X, (HEIGHT - y - 1) * PIXEL_PER_UNIT_Y);

                                m_Blocks[y][x] = tile.GetComponent<Block>();
                            }
                            break;

                        case 5:
                            {
                                tile = Instantiate(m_WallPrefab2, tileMapParent.transform);
                                tile.transform.position = new Vector2(x * PIXEL_PER_UNIT_X, (HEIGHT - y - 1) * PIXEL_PER_UNIT_Y);

                                m_Blocks[y][x] = tile.GetComponent<Block>();
                            }
                            break;

                        case 6:
                            {
                                tile = Instantiate(m_WallPrefab3, tileMapParent.transform);
                                tile.transform.position = new Vector2(x * PIXEL_PER_UNIT_X, (HEIGHT - y - 1) * PIXEL_PER_UNIT_Y);

                                m_Blocks[y][x] = tile.GetComponent<Block>();
                            }
                            break;

                        case 7:
                            {
                                tile = Instantiate(m_WallPrefab4, tileMapParent.transform);
                                tile.transform.position = new Vector2(x * PIXEL_PER_UNIT_X, (HEIGHT - y - 1) * PIXEL_PER_UNIT_Y);

                                m_Blocks[y][x] = tile.GetComponent<Block>();
                            }
                            break;

                        case 8:
                            {
                                tile = Instantiate(m_BlockPrefab1, tileMapParent.transform);
                                tile.transform.position = new Vector2(x * PIXEL_PER_UNIT_X, (HEIGHT - y - 1) * PIXEL_PER_UNIT_Y);

                                m_Blocks[y][x] = tile.GetComponent<Block>();
                            }
                            break;

                        case 9:
                            {
                                tile = Instantiate(m_BlockPrefab2, tileMapParent.transform);
                                tile.transform.position = new Vector2(x * PIXEL_PER_UNIT_X, (HEIGHT - y - 1) * PIXEL_PER_UNIT_Y);

                                m_Blocks[y][x] = tile.GetComponent<Block>();
                            }
                            break;

                        case 10:
                            {
                                tile = Instantiate(m_BlockPrefab3, tileMapParent.transform);
                                tile.transform.position = new Vector2(x * PIXEL_PER_UNIT_X, (HEIGHT - y - 1) * PIXEL_PER_UNIT_Y);

                                m_Blocks[y][x] = tile.GetComponent<Block>();
                            }
                            break;

                        case 11:
                            {
                                tile = Instantiate(m_WallPrefab5, tileMapParent.transform);
                                tile.transform.position = new Vector2(x * PIXEL_PER_UNIT_X, (HEIGHT - y - 1) * PIXEL_PER_UNIT_Y);

                                m_Blocks[y][x] = tile.GetComponent<Block>();
                            }
                            break;
                    }
                }
            }

            tileMapParent.transform.position = GetPosition();

            return stageBlocks;
        }

        public Block GetBlock(int x, int y)
        {
            if (x < 0 || x >= WIDTH || y < 0 || y >= HEIGHT)
            {
                return null;
            }

            return m_Blocks[y][x];
        }

        public Vector2Int GetStartPosition(int stage)
        {
            int[][] stageBlocks = StageBlocks[stage];

            for (int i = 0; i < stageBlocks.Length; i++)
            {
                for (int j = 0; j < stageBlocks[i].Length; j++)
                {
                    if (stageBlocks[i][j] == 1)
                    {
                        return new(j, i);
                    }
                }
            }

            throw new System.Exception("Start position not found");
        }

        public List<Vector2Int> GetWallPositions(int stage)
        {
            int[][] stageBlocks = StageBlocks[stage];

            List<Vector2Int> wallPositions = new();

            for (int i = 0; i < stageBlocks.Length; i++)
            {
                for (int j = 0; j < stageBlocks[i].Length; j++)
                {
                    if (stageBlocks[i][j] == 9)
                    {
                        wallPositions.Add(new(j, i));
                    }
                }
            }

            return wallPositions;
        }

        public Player AddPlayer(GameObject playerPrefab, Vector2 position)
        {
            GameObject moverInstance = Instantiate(playerPrefab, m_Parent2);
            Player player = moverInstance.GetComponent<Player>();
            player.SetPosition((int)position.x, (int)position.y, true);

            return player;
        }

        public Soul AddSoul(GameObject soulPrefab, Vector2 position)
        {
            GameObject soulInstance = Instantiate(soulPrefab, m_Parent2);
            Soul soul = soulInstance.GetComponent<Soul>();
            soul.SetPosition((int)position.x, (int)position.y, true);

            return soul;
        }

        private Vector2 GetPosition()
        {
            int xPos = 960 - WIDTH * PIXEL_PER_UNIT_X / 2;
            int yPos = 540 - HEIGHT * PIXEL_PER_UNIT_Y / 2;

            return new(xPos, yPos);
        }
    }
}
