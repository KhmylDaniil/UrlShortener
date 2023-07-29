namespace UrlShortener.MVC.Models
{
    /// <summary>
    /// ������ ����������� ���������� ���������� ������
    /// </summary>
    public class ErrorViewModel
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
