using TeamJustFour.MoveOneStep.Game;
using TeamJustFour.MoveOneStep.UI;
using UnityEngine;

namespace TeamJustFour.MoveOneStep.Module
{
    public class InGameSceneGameManager : Singleton<InGameSceneGameManager>
    {
        [SerializeField] private GameObject m_PlayerPrefab;
        [SerializeField] private GameObject m_SoulPrefab;
        [SerializeField] private UI_InGameSceneRoot m_UIRoot;

        private TilemapGenerator m_TilemapGenerator;
        private Soul m_Soul;

        public Player Player;

        public UI_InGameSceneRoot UIRoot
        {
            get
            {
                return m_UIRoot;
            }
        }

        public int Stage
        {
            get
            {
                return PlayerPrefsManager.Instance.GetStage();
            }
        }

        public bool CanMove(int x, int y)
        {
            if (m_TilemapGenerator.StageBlocks[Stage][y][x] == 0 || m_TilemapGenerator.StageBlocks[Stage][y][x] == 1 
                || m_TilemapGenerator.StageBlocks[Stage][y][x] == 2 || m_TilemapGenerator.StageBlocks[Stage][y][x] == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RemoveBlockCoordinate(int x, int y)
        {
            m_TilemapGenerator.StageBlocks[Stage][y][x] = 0;
        }

        public Block GetBlock(int x, int y)
        {
            if (m_TilemapGenerator.StageBlocks[Stage][y][x] == 0)
                return null;

            return m_TilemapGenerator.GetBlock(x, y);
        }

        public void GameOver()
        {
            Destroy(Player.gameObject);
            Destroy(m_Soul.gameObject);

            m_UIRoot.ShowFailedPopup();
        }

        public void GameClear()
        {
            Destroy(Player.gameObject);
            Destroy(m_Soul.gameObject);

            m_UIRoot.ShowClearPopup();
        }

        protected override void Awake()
        {
            base.Awake();

            m_TilemapGenerator = gameObject.GetComponent<TilemapGenerator>();
        }

        private void Start()
        {
            m_TilemapGenerator.GenerateTilemap(Stage);

            Vector2Int startPosition = m_TilemapGenerator.GetStartPosition(Stage);
            //List<Vector2Int> wallPositions = m_TilemapGenerator.GetWallPositions(Stage);

            Player = m_TilemapGenerator.AddPlayer(m_PlayerPrefab, new(startPosition.x, startPosition.y));

            m_Soul = m_TilemapGenerator.AddSoul(m_SoulPrefab, new(startPosition.x, startPosition.y));
            m_Soul.FindNextDirection(m_TilemapGenerator.NPCPaths[Stage], m_TilemapGenerator.StageBlocks[Stage]);
        }
    }
}
