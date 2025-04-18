namespace RazorClassLibrary1.Services
{
    public class NotifierService
    {
        /// <summary>
        /// Notifier event.
        /// </summary>
        public event Func<string, object?, Task>? Notifier;

        /// <summary>
        /// <see cref="SendNotification"/>
        /// https://www.perplexity.ai/search/como-notificar-en-blazor-a-un-56TeSZNlR_ahtAZ4hQaeVg
        /// </summary>
        /// <param name="key">The key value</param>
        /// <param name="value">The object value</param>
        /// <returns>An instance of the <see cref="Task"/> object.</returns>
        public async Task SendNotification(string key, object? value)
        {
            if (Notifier != null)
                await Notifier.Invoke(key, value);
        }
    }
}
