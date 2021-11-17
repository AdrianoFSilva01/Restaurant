<template>
    <div class="flex p-8 justify-between items-center">
        <div class="leading-3 text-sm">
            Order
            <br />
            Now
        </div>
        <div class="flex justify-center h-14">
            <img src="@/Assets/Images/logo.svg" />
        </div>
        <div class="hamburgerMenu" @click="hamburgerMenuOnClick">
            <hr />
            <hr />
            <hr />
        </div>
    </div>
</template>

<script lang="ts" src="./Header.ts" />

<style lang="postcss" src="./Header.pcss" />
