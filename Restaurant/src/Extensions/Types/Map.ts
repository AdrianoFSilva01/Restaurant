export { };

declare global {
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    interface Map<K, V> {
        lastKey(): K;
        getKeys(): Array<K>;
    }
}

Map.prototype.lastKey = function lastKey<K>(): K {
    const index: number = this.size - 1;
    const lastKey: K = [...this.entries()][index][0];
    return lastKey;
}

Map.prototype.getKeys = function getKeys<K>(): Array<K> {
    const keys: Array<K> = [];

    for(const item of this) {
        keys.push(item[0]);
    }

    return keys;
}
