<template>
    <div id="app">
        <Header @hamburger-menu-clicked="oi" />
        <HelloWorld />
    </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import HelloWorld from "@/Components/HelloWorld.vue";
import Header from "@/Views/Header.vue";

@Options({
    components: {
        HelloWorld,
        Header
    }
})
export default class MasterPage extends Vue {
    oi(): void {
        console.log("oi");
    }
}
</script>
