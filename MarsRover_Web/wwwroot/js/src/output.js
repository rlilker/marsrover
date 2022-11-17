export default class Output {
    static showOutput(text) {
        this.#showOutputHtml(`<p class="white">${this.#cleanseOutput(text)}</p>`);
        
    }
    static showError(text) {
        this.#showOutputHtml(`<p class="red">${this.#cleanseOutput(text)}</p>`);
    }
    static clear() {
        document.getElementById("output").innerText = "";
    }

    static #showOutputHtml(outputHtml) {
        var outputWindow = document.getElementById("output");
        outputWindow.insertAdjacentHTML('beforeend', outputHtml);
    }
    static #cleanseOutput(text) {
        return `${text.replace(/\n/, "<br />")}`;
    }
}