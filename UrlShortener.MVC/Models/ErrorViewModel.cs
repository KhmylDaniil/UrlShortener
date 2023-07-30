namespace UrlShortener.MVC.Models
{
    /// <summary>
    /// ������ ����������� ���������� ���������� ������
    /// </summary>
    public sealed class ErrorViewModel
    {
        /// <summary>
        /// ��������� �� ������
        /// </summary>
        public string Message { get; set; }

        public ErrorViewModel(Exception ex)
        {
            Message = ex.Message;
        }
    }
}
