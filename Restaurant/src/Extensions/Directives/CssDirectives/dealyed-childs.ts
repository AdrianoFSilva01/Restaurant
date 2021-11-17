import { DirectiveBinding } from "@vue/runtime-core";

export default {
    mounted: function (el: HTMLElement, binding: DirectiveBinding): void {
        const delayValue: number = binding.value[0];
        const className: string = binding.value[1];

        const childs: HTMLCollection = el.getElementsByClassName(className);

        for(let i: number = 0; i < childs.length; i++) {
            (childs[i] as HTMLElement).style.transitionDelay = delayValue * i + "s";
        }
    }
};