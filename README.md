# Converter

Dockerized Azure function to convert HTML to PDFs.

Images at https://hub.docker.com/r/jonnypowellrazor/html_pdf_func.

To build, you will need to get a copy of chromium and place it in `src/Converter/.local-chromium/Linux-{version}/chrome-linux`.
See [here](https://github.com/puppeteer/puppeteer/issues/1425) for more information.
