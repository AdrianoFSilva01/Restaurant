import { DirectiveBinding } from "@vue/runtime-core";

export default {
    updated: function (el: HTMLElement, binding: DirectiveBinding): void {
        const delayValue: number = binding.value[0];
        const className: string = binding.value[1];
        const classToAdd: string = binding.value[2];

        const childs: HTMLCollection = el.getElementsByClassName(className);

        for(let i: number = 0; i < childs.length; i++) {
            setTimeout(() => {
                (childs[i] as HTMLElement).classList.add(classToAdd);
            }, (delayValue * i) * 1000);
        }
    }
};