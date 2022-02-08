<template>
    <div ref="contextMenu" v-click-outside="hide" class="hidden">
        <slot />
    </div>
</template>

<script lang="ts" src="./ContextMenu.ts" />