<template>
    <div class="flex p-8 items-center">
        <div class="leading-3 text-sm flex-1">
            Order
            <br />
            Now
        </div>
        <div class="flex justify-center h-14 flex-1">
            <img src="@/Assets/Images/logo.svg" />
        </div>
        <div class="flex-1 flex justify-end">
            <div class="hamburgerMenu" @click="hamburgerMenuOnClick">
                <hr />
                <hr />
                <hr />
            </div>
        </div>
    </div>
</template>

<script lang="ts" src="./Header.ts" />

<style lang="postcss" src="./Header.pcss" />
