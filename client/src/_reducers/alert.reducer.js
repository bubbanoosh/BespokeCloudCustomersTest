import { alertActionTypes as actionTypes } from '../_constants';

export function alert(state = {}, action) {
    switch (action.type) {
        case actionTypes.SUCCESS:
            return {
                type: 'alert-success',
                message: action.message
            };
        case actionTypes.ERROR:
            return {
                type: 'alert-danger',
                message: action.message
            };
        case actionTypes.CLEAR:
            return {};
        default:
            return state;
    }
}