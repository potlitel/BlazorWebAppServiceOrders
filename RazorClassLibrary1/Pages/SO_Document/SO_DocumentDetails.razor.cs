using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using RazorClassLibrary1.Dtos;

namespace RazorClassLibrary1.Pages.SO_Document
{
    public partial class SO_DocumentDetails
    {
        [Parameter]
        public ServiceOrderDocumentDto Document { get; set; } = new();

        [Parameter]
        public bool IsSideDialog { get; set; } = false;

        [Parameter]
        public string DocUrlViewer { get; set; }

        private string ReadUrl { get; set; }

        protected override void OnInitialized()
        {
            ReadUrl = "http://127.0.0.1:10000/devstoreaccount1/sodocuments/1746122159_mirada%20AD%20w%202008%20usuarios%20y%20equipos.pdf?sv=2025-01-05&se=2025-05-11T20%3A53%3A27Z&sr=b&sp=r&sig=7uUWL4XJ1Du8vKdk36PWeQXzx7doiV9E8dL20JZalzM%3D";
            StateHasChanged();
        }

        #region ToDelete
        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    if (firstRender)
        //    {
        //        //DocumentStream = await DownloadServiceOrderDocumentAsStreamService.Handle(Document.Name);
        //        await NewMethod();
        //    }
        //}

        //private async Task NewMethod()
        //{
        //    var extension = Path.GetExtension(Document.Name);
        //    //Content = DocumentStream;
        //    //var strRef = new DotNetStreamReference(DocumentStream);
        //    //await JS.InvokeVoidAsync("setSource", "docViewer", strRef);
        //    //await JS.InvokeVoidAsync("setIframeSrc", "iframeRef", "https://x.com/David_eficaz/status/1920869006598119827");
        //}
        #endregion

        private string eventLog { get; set; } = $"Last event: ..., CurrentPage: 0, TotalPages: 0";

        private void OnDocumentLoaded(PdfViewerEventArgs args)
            => eventLog = $"Last event: OnDocumentLoaded, CurrentPage: {args.CurrentPage}, TotalPages: {args.TotalPages}";

        private void OnPageChanged(PdfViewerEventArgs args)
            => eventLog = $"Last event: OnPageChanged, CurrentPage: {args.CurrentPage}, TotalPages: {args.TotalPages}";
    }
}
