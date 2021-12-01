<template>
    <div class="inlineListcontainer">
        <template v-for="(name, index) in list" :key="index">
            <div class="listItem">
                <template v-if="index > 0">
                    <hr class="w-4 mx-2 border-black" />
                </template>
                <p class="listText" :class="{ 'opacity-30' : index !== currentIndex }" @click="onClick">
                    {{ name }}
                </p>
            </div>
        </template>
    </div>
</template>

<script lang="ts" src="./InlineList.ts" />

<style scoped lang="postcss" src="./InlineList.pcss" />
