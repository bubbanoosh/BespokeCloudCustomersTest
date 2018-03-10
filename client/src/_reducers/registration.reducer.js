import { registrationActionTypes as actionTypes } from '../_constants';

export function registration(state = {}, action) {
  switch (action.type) {
    case actionTypes.REGISTER_REQUEST:
      return { registering: true };
    case actionTypes.REGISTER_SUCCESS:
      return {};
    case actionTypes.REGISTER_FAILURE:
      return {};
    default:
      return state;
  }
}