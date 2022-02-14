import { DirectiveBinding } from "@vue/runtime-core";
import { nextTick } from "vue";

export default {
    mounted: function(el: HTMLElement, binding: DirectiveBinding): void {
        const delayValue: number = binding.value[0];
        const className: string = binding.value[1];
        const classToAdd: string = binding.value[2];
        const execOnMount: boolean = binding.value[3];

        if(execOnMount) {
            nextTick(() => {
                const childs: HTMLCollection = el.getElementsByClassName(className);

                for(let i: number = 0; i < childs.length; i++) {
                    setTimeout(() => {
                        (childs[i] as HTMLElement).classList.add(classToAdd);
                    }, (delayValue * i) * 1000);
                }
            });
        }
    },
    updated: function(el: HTMLElement, binding: DirectiveBinding): void {
        const delayValue: number = binding.value[0];
        const className: string = binding.value[1];
        const classToAdd: string = binding.value[2];

        const childs: HTMLCollection = el.getElementsByClassName(className);

        for(let i: number = 0; i < childs.length; i++) {
            if(childs[i].classList.contains(classToAdd)) {
                return;
            }

            setTimeout(() => {
                (childs[i] as HTMLElement).classList.add(classToAdd);
            }, (delayValue * i) * 1000);
        }
    },
    unmounted: function(el: HTMLElement, binding: DirectiveBinding): void {
        const className: string = binding.value[1];
        const classToAdd: string = binding.value[2];
        const childs: HTMLCollection = el.getElementsByClassName(className);

        for(let i: number = 0; i < childs.length; i++) {
            childs[i].classList.remove(classToAdd);
        }
    }
};