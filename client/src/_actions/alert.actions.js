import { alertActionTypes as actionTypes } from '../_constants';

export const alertActions = {
    success,
    error,
    clear
};

function success(message) {
    return { type: actionTypes.SUCCESS, message };
}

function error(message) {
    return { type: actionTypes.ERROR, message };
}

function clear() {
    return { type: actionTypes.CLEAR };
}