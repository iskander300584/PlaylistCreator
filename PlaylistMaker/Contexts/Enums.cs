

namespace PlaylistMaker.Contexts
{
    /// <summary>
    /// Статус
    /// </summary>
    public enum ItemStatus
    {
        /// <summary>
        /// Существует
        /// </summary>
        Exist,

        /// <summary>
        /// Не существует
        /// </summary>
        NotExist,

        /// <summary>
        /// Не в отностительном пути
        /// </summary>
        NotRelative,

        /// <summary>
        /// Файл не находится в папке
        /// </summary>
        NotInFolder
    }


    /// <summary>
    /// Класс работы с перечислениями
    /// </summary>
    internal class Enums
    {
        /// <summary>
        /// Получить текст для соответствующего значения Статуса
        /// </summary>
        /// <param name="status">статус</param>
        internal static string GetItemStatus_Text(ItemStatus status)
        {
            switch(status)
            {
                case ItemStatus.NotExist:
                    return "Файл или папка не найдена";

                case ItemStatus.NotRelative:
                    return "Папка не находится по вложенному пути, относительно файла плей-листа";

                case ItemStatus.NotInFolder:
                    return "Файл не находится в указанной папке";

                default:
                    return "Конфликтов не выявлено";
            }
        }
    }
}