// Copyright 2009 the Sputnik authors.  All rights reserved.
/**
 * The production x *= y is the same as the production x = x * y
 *
 * @path ch11/11.13/11.13.2/S11.13.2_A4.1_T2.9.js
 * @description Type(x) is different from Type(y) and both types vary between Boolean (primitive or object) and Null
 */

//CHECK#1
x = true;
x *= null;
if (x !== 0) {
  $ERROR('#1: x = true; x *= null; x === 0. Actual: ' + (x));
}

//CHECK#2
x = null;
x *= true;
if (x !== 0) {
  $ERROR('#2: x = null; x *= true; x === 0. Actual: ' + (x));
}

//CHECK#3
x = new Boolean(true);
x *= null;
if (x !== 0) {
  $ERROR('#3: x = new Boolean(true); x *= null; x === 0. Actual: ' + (x));
}

//CHECK#4
x = null;
x *= new Boolean(true);
if (x !== 0) {
  $ERROR('#4: x = null; x *= new Boolean(true); x === 0. Actual: ' + (x));
}

