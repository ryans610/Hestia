"use strict";

class LuxonCountDown {
    constructor(time, intervalCallback, endCallback, checkInterval = 500) {
        if (!window.luxon) {
            this.#hasLuxon = false;
            return;
        }
        this.#config = Object.assign(this.#config, { time, intervalCallback, endCallback, checkInterval });
    }

    #hasLuxon = true;
    #config = {
        time: undefined,
        intervalCallback: undefined,
        endCallback: undefined,
        checkInterval: undefined,
        startTime: undefined,
        intervalId: undefined,
    };

    get isCountingDown() {
        this.#checkHasLuxon();

        return typeof this.#config.intervalId !== "undefined";
    }

    start() {
        this.#checkHasLuxon();

        this.stop();
        this.#config.startTime = luxon.DateTime.now();
        this.#config.intervalId = setInterval(() => {
            let current = luxon.DateTime.now();
            let diff = current.diff(this.#config.startTime, "seconds").seconds;
            if (diff >= this.#config.time) {
                this.stop();
                this.#config.endCallback();
            } else {
                this.#config.intervalCallback(this.#config.time - diff);
            }
        }, this.#config.checkInterval);
    }

    stop() {
        this.#checkHasLuxon();

        clearInterval(this.#config.intervalId);
        this.#config.intervalId = undefined;
    }

    #checkHasLuxon() {
        if (!this.#hasLuxon) {
            throw `"Luxon" is not included.`;
        }
    }
}
