namespace Tutorial.LifeGame
{
    /// <summary>
    /// ���C�t�Q�[���̘_���f�[�^�B
    /// </summary>
    public class LifeGameData
    {
        /// <summary>
        /// �s���B
        /// </summary>
        public int Rows { get; }

        /// <summary>
        /// �񐔁B
        /// </summary>
        public int Columns { get; }

        // �w��̍s�ԍ��E��ԍ��̏��
        public LLCellState this[int row, int column] // �C���f�N�T�[
        {
            get => _cells[row, column];
            set => _cells[row, column] = value;
        }
        private LLCellState[,] _cells;
        /// <summary>
        /// �R���X�g���N�^�[�B
        /// </summary>
        /// <param name="rows">�s���B</param>
        /// <param name="columns">�񐔁B</param>
        public LifeGameData(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            _cells = new LLCellState[Rows, Columns];
        }

        /// <summary>
        /// �K���Ƀ����_���Ő����Ă���Z����ݒu����B
        /// </summary>
        /// <param name="aliveProbability">�����Ă���Z���̏o�����B</param>
        public void InitializeRandom(double aliveProbability)
        {
            var random = new System.Random();
            for (var r = 0; r < Rows; r++)
            {
                for (var c = 0; c < Columns; c++)
                {
                    _cells[r, c] = random.NextDouble() > aliveProbability
                        ? LLCellState.Alive : LLCellState.Dead;
                }
            }
        }

        public void Step(LifeGameData next)
        {
            for (var r = 0; r < Rows; r++)
            {
                for (var c = 0; c < Columns; c++)
                {
                    var cell = _cells[r, c];

                    // ���͂̐����Ă���Z����
                    var ac = GetNeighborCount(r, c, LLCellState.Alive);

                    if (cell == LLCellState.Dead) // ����ł���Z��
                    {
                        // ���͂ɐ������Z����3����Βa��
                        if (ac == 3) { next[r, c] = LLCellState.Alive; }
                    }
                    else // �����Ă���Z��
                    {
                        // ���͂ɐ������Z����2�`3����ΐ����A����ȊO�͎���
                        if (ac == 2 || ac == 3) { next[r, c] = LLCellState.Alive; }
                        else { next[r, c] = LLCellState.Dead; }
                    }
                }
            }
        }

        /// <summary>
        /// �w��̍s�ԍ��E��ԍ��̎��͂̎w��̏�Ԃ̃Z������Ԃ��B
        /// </summary>
        /// <param name="row">�s�ԍ��B</param>
        /// <param name="column">��ԍ��B</param>
        /// <param name="state">�Z���̏�ԁB</param>
        /// <returns>���͂� <paramref name="state"/> ��Ԃ̃Z������Ԃ��B</returns>
        public int GetNeighborCount(int row, int column, LLCellState state)
        {
            int count = 0;

            // ���͂̃Z�����w��̏�ԂȂ� count ���C���N�������g����B
            { if (TryGetCell(row - 1, column - 1, out LLCellState x) && x == state) { count++; } }
            { if (TryGetCell(row - 1, column, out LLCellState x) && x == state) { count++; } }
            { if (TryGetCell(row - 1, column + 1, out LLCellState x) && x == state) { count++; } }
            { if (TryGetCell(row, column - 1, out LLCellState x) && x == state) { count++; } }
            { if (TryGetCell(row, column + 1, out LLCellState x) && x == state) { count++; } }
            { if (TryGetCell(row + 1, column - 1, out LLCellState x) && x == state) { count++; } }
            { if (TryGetCell(row + 1, column, out LLCellState x) && x == state) { count++; } }
            { if (TryGetCell(row + 1, column + 1, out LLCellState x) && x == state) { count++; } }

            return count;
        }

        /// <summary>
        /// �w��̍s�ԍ��E��ԍ��̃Z�����擾����B
        /// </summary>
        /// <param name="row">�s�ԍ��B</param>
        /// <param name="column">��ԍ��B</param>
        /// <param name="state">�Z���B</param>
        /// <returns>�Z�����擾�ł���� true�B�����łȂ���� false�B</returns>
        public bool TryGetCell(int row, int column, out LLCellState state)
        {
            if (row < 0 || column < 0 || row >= _cells.GetLength(0) || column >= _cells.GetLength(1))
            {
                state = default;
                return false;
            }

            state = _cells[row, column];
            return true;
        }
    }
}

public enum LLCellState
{
    Dead,
    Alive,
}