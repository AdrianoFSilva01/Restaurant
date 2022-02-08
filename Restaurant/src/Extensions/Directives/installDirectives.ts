import delayedChilds from "@/Extensions/Directives/CssDirectives/dealyed-childs";
import clickOutside from "@/Extensions/Directives/JsDirectives/click-outside";
import { App } from "vue";
import firstHover from "./CssDirectives/first-hover";
import inViewport from "./CssDirectives/in-viewport";

export default function installDirectives(app: App<Element>): void {
    app.directive("delayed-childs", delayedChilds);
    app.directive("in-viewport", inViewport);
    app.directive("click-outside", clickOutside);
    app.directive("first-hover", firstHover);
}