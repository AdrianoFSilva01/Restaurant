<template>
    <div class="inline-list-container">
        <template v-for="(name, index) in list" :key="index">
            <div class="list-item">
                <template v-if="index > 0">
                    <hr class="w-4 mx-2 border-black dark:border-white" />
                </template>
                <p class="list-text" :class="{ 'opacity-30' : index !== currentIndex }" @click="onClick">
                    {{ name }}
                </p>
            </div>
        </template>
    </div>
</template>

<script lang="ts" src="./InlineList.ts" />

<style scoped lang="scss" src="./InlineList.scss" />
