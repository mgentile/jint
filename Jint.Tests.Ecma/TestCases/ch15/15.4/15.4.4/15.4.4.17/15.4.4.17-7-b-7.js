/// Copyright (c) 2012 Ecma International.  All rights reserved. 
/**
 * @path ch15/15.4/15.4.4/15.4.4.17/15.4.4.17-7-b-7.js
 * @description Array.prototype.some - properties can be added to prototype after current position are visited on an Array
 */


function testcase() {
        function callbackfn(val, idx, obj) {
            if (idx === 1 && val === 6.99) {
                return true;
            } else {
                return false;
            }
        }
        var arr = [0, , 2];

        Object.defineProperty(arr, "0", {
            get: function () {
                Object.defineProperty(Array.prototype, "1", {
                    get: function () {
                        return 6.99;
                    },
                    configurable: true
                });
                return 0;
            },
            configurable: true
        });

        try {
            return arr.some(callbackfn);
        } finally {
            delete Array.prototype[1];
        }
    }
runTestCase(testcase);
