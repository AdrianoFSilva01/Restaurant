export { };

declare global {
    interface HTMLElement {
        lastElement(): HTMLElement;
        firstElement(): HTMLElement;
        parent(): HTMLElement;
        previousSiblingElement(): HTMLElement;
        nextSiblingElement(): HTMLElement;
        getLeftPosition(): number;
        getWidth(): number;
        getHeight(): number;
        getMarginLeft(): number;
        getMarginRight(): number;
        getPaddingTop(): number;
        getTransition(): string;
    }
}

HTMLElement.prototype.lastElement = function lastElement(): HTMLElement {
    return this.lastElementChild as HTMLElement;
}

HTMLElement.prototype.firstElement = function firstElement(): HTMLElement {
    return this.firstElementChild as HTMLElement;
}

HTMLElement.prototype.parent = function parent(): HTMLElement {
    return this.parentElement as HTMLElement;
}

HTMLElement.prototype.previousSiblingElement = function previousSiblingElement(): HTMLElement {
    return this.previousElementSibling as HTMLElement;
}

HTMLElement.prototype.nextSiblingElement = function nextSiblingElement(): HTMLElement {
    return this.nextElementSibling as HTMLElement;
}

HTMLElement.prototype.getLeftPosition = function getLeftPosition(): number {
    return this.getBoundingClientRect().left;
}

HTMLElement.prototype.getWidth = function getWidth(): number {
    return this.getBoundingClientRect().width;
}

HTMLElement.prototype.getHeight = function getHeight(): number {
    return this.getBoundingClientRect().height;
}

HTMLElement.prototype.getMarginLeft = function getMarginLeft(): number {
    return parseInt(window.getComputedStyle(this).marginLeft);
}

HTMLElement.prototype.getMarginRight = function getMarginRight(): number {
    return parseInt(window.getComputedStyle(this).marginRight);
}

HTMLElement.prototype.getPaddingTop = function getPaddingTop(): number {
    return parseInt(window.getComputedStyle(this).paddingTop);
}

HTMLElement.prototype.getTransition = function getTransition(): string {
    return window.getComputedStyle(this).transition;
}
