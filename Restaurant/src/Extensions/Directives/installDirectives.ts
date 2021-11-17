import delayedChilds from "@/Extensions/Directives/CssDirectives/dealyed-childs";
import { App } from "vue";

export default function installDirectives(app: App<Element>): void {
    app.directive("delayed-childs", delayedChilds);
}