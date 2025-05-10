function setIframeSrc(iframeElement, src) {
    iframeElement.src = src;
}

function setIframeContent(iframeElement, content) {
    const blob = new Blob([content], { type: 'text/html' });
    const url = URL.createObjectURL(blob);
    iframeElement.src = url;
}