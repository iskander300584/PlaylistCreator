
namespace PlaylistMaker.Contexts
{
    /// <summary>
    /// Используемые константы
    /// </summary>
    internal static class Constants
    {
        /// <summary>
        /// Расширение файла плей-листа
        /// </summary>
        internal const string PLAYLIST_EXTENSION = ".m3u";


        /// <summary>
        /// Заголовок файла плейлиста
        /// </summary>
        internal const string PLAYLIST_HEADER = @"#EXTM3U";


        /// <summary>
        /// Заголовок метаданных элемента плейлиста
        /// </summary>
        internal const string PLAYLIST_ITEM_META = @"#EXTINF";
    }
}