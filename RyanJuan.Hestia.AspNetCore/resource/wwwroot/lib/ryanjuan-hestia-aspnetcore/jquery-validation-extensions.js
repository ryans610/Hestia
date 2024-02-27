"use strict";

document.addEventListener("DOMContentLoaded", function () {
    if (window.jQuery &&
        window.jQuery.validator) {
        class jqueryValidatorSetting {
            constructor(name, method, paramArray = undefined) {
                this.#name = name;
                this.#method = method;
                this.#paramArray = HestiaCommon.isNullOrUndefined(paramArray) ? [] : paramArray;
            }

            #name = undefined;
            #method = undefined;
            #paramArray = undefined;

            get name() { return this.#name; }
            get method() { return this.#method; }
            get paramArray() { return this.#paramArray; }
        }

        const TaiwanIdNoAlphabetScore = [
            1, 10, 19, 28, 37, 46, 55, 64, 39, 73, 82, 2, 11,
            20, 48, 29, 38, 47, 56, 65, 74, 83, 21, 3, 12, 30,
        ];
        const TaiwanLegalIdNoWeights = [
            1, 2, 1, 2, 1, 2, 4, 1,
        ];

        const settings = [
            new jqueryValidatorSetting("digits", function (value, element) {
                if (this.optional(element)) {
                    return true;
                }
                const stringValue = typeof (value) === "string" ? value : value.toString();
                for (const i = 0; i < stringValue.length; i += 1) {
                    const c = stringValue[i];
                    if (c < '0' || c > '9') {
                        return false;
                    }
                }
                return true;
            }),
            new jqueryValidatorSetting("digitsAnd", function (value, element, parameters) {
                if (this.optional(element)) {
                    return true;
                }
                const stringValue = typeof (value) === "string" ? value : value.toString();
                for (const i = 0; i < stringValue.length; i += 1) {
                    const c = stringValue[i];
                    if (c < '0' || c > '9') {
                        if (!parameters.characters.includes(c)) {
                            return false;
                        }
                    }
                }
                return true;
            }, ["characters"]),
            new jqueryValidatorSetting("utf8StringByteLength", function (value, element, parameters) {
                if (this.optional(element)) {
                    return true;
                }
                const byteLength = new Blob([value]).size;
                return byteLength <= parameters.maximum && byteLength >= parameters.minimum;
            }, ["maximum", "minimum"]),
            new jqueryValidatorSetting("greaterThan", function (value, element, parameters) {
                if (this.optional(element)) {
                    return true;
                }
                const nValue = Number(value);
                if (Number.isNaN(nValue)) {
                    return true;
                }
                return nValue > Number(parameters.compareValue);
            }, ["compareValue"]),
            new jqueryValidatorSetting("lessThan", function (value, element, parameters) {
                if (this.optional(element)) {
                    return true;
                }
                const nValue = Number(value);
                if (Number.isNaN(nValue)) {
                    return true;
                }
                return nValue < Number(parameters.compareValue);
            }, ["compareValue"]),
            new jqueryValidatorSetting("taiwanIdNo", function (value, element) {
                if (this.optional(element)) {
                    return true;
                }
                if (value.length != 10) {
                    return false;
                }
                value = value.toUpperCase();
                if (!/^[A-Z](1|2)\d{8}$/.test(value)) {
                    return false;
                }
                let total = TaiwanIdNoAlphabetScore[value[0].charCodeAt(0) - 65];
                for (let i = 1; i <= 8; i += 1) {
                    total += (value[i] & 0b1111) * (9 - i);
                }
                total += value[9] & 0b1111;
                return total % 10 === 0;
            }),
            new jqueryValidatorSetting("taiwanLegalIdNo", function (value, element) {
                if (this.optional(element)) {
                    return true;
                }
                if (value.length != 8) {
                    return false;
                }
                if (!/^\d{8}$/.test(value)) {
                    return false;
                }
                let intValues = [];
                let total = 0;
                for (let i = 0; i < 8; i += 1) {
                    intValues[i] = value[i] & 0b1111;
                    const product = intValues[i] * TaiwanLegalIdNoWeights[i];
                    let result = parseInt(product / 10, 10) + product % 10;
                    if (result === 10) {
                        result = 1;
                    }
                    total += result;
                }
                const remainder = total % 10;
                return remainder === 0 || (remainder === 9 && intValues[6] === 7);
            }),
            new jqueryValidatorSetting("equalsToProperty", function (value, element, parameters) {
                if (this.optional(element)) {
                    return true;
                }
                const name = element.name;
                const nameBaseLength = name.lastIndexOf(".");
                const nameBase = nameBaseLength === -1 ? "" : name.substring(0, nameBaseLength + 1);
                const targetName = `${nameBase}${parameters.propertyName}`;
                const targetElement = $(element).closest("form").find(`[name="${targetName}"]`);
                const targetValue = targetElement.val();
                if (typeof value === "undefined" || value === null) {
                    return typeof targetValue === "undefined" || targetValue === null;
                }
                return value.toString() === targetValue.toString();
            }, ["propertyName"]),
        ];

        settings.forEach(x => {
            window.jQuery.validator.addMethod(x.name, x.method);
        });

        if (window.jQuery.validator.unobtrusive) {
            settings.forEach(x => {
                window.jQuery.validator.unobtrusive.adapters.add(
                    x.name,
                    x.paramArray,
                    function (options) {
                        options.rules[x.name] = options.params;
                        options.messages[x.name] = options.message;
                    }
                );
            });
        }
    }
});
