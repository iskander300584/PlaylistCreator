

namespace UnitTests
{
    /// <summary>
    /// Используемые для тестирования константы
    /// </summary>
    internal static class Constants
    {
        /// <summary>
        /// Путь к файлу плей-листа
        /// </summary>
        internal const string PATH_TO_PLAYLIST_FILE = @"e:\TestPlaylistCreator\";


        /// <summary>
        /// Путь к папке с музыкой
        /// </summary>
        internal const string PATH_TO_MUSIC_FOLDER = @"e:\TestPlaylistCreator\Rock\";


        /// <summary>
        /// Первый файл
        /// </summary>
        internal const string FILE_1_PATH = @"e:\TestPlaylistCreator\Rock\Queen - Killer Queen.mp3";


        /// <summary>
        /// Второй файл
        /// </summary>
        internal const string FILE_2_PATH = @"e:\TestPlaylistCreator\Rock\Queen - Save Me.mp3";


        /// <summary>
        /// Третий файл
        /// </summary>
        internal const string FILE_3_PATH = @"e:\TestPlaylistCreator\Rock\Сплин - Линия жизни.mp3";


        /// <summary>
        /// Список файлов для тестирования
        /// </summary>
        internal static string[] FILES = { FILE_1_PATH, FILE_2_PATH, FILE_3_PATH };
    }
}