using UIKit;

namespace View.iOS;

/// <summary>
/// Класс приложения.
/// </summary>
public class Application
{
    /// <summary>
    /// Запускает основной поток приложения.
    /// </summary>
    /// <param name="args">Аргументы.</param>
    static void Main(string[] args)
    {
        UIApplication.Main(args, null, typeof(AppDelegate));
    }
}
