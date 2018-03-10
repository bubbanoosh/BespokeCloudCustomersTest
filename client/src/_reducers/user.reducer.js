import { userActionTypes as actionTypes } from '../_constants';

export function users(state = {}, action) {
  switch (action.type) {
    case actionTypes.GETALL_USERS_REQUEST:
      return {
        loading: true
      };
    case actionTypes.GETALL_USERS_SUCCESS:
      return {
        items: action.users
      };
    case actionTypes.GETALL_USERS_FAILURE:
      return { 
        error: action.error
      };
    case actionTypes.REMOVE_USER_REQUEST:
      // add 'deleting:true' property to user being deleted
      return {
        ...state,
        items: state.items.map(user =>
          user.id === action.id
            ? { ...user, deleting: true }
            : user
        )
      };
    case actionTypes.REMOVE_USER_SUCCESS:
      // remove deleted user from state
      return {
        items: state.items.filter(user => user.id !== action.id)
      };
    case actionTypes.REMOVE_USER_FAILURE:
      // remove 'deleting:true' property and add 'deleteError:[error]' property to user 
      return {
        ...state,
        items: state.items.map(user => {
          if (user.id === action.id) {
            // make copy of user without 'deleting:true' property
            // eslint-disable-next-line no-unused-vars
            const { deleting, ...userCopy } = user;
            // return copy of user with 'deleteError:[error]' property
            return { ...userCopy, deleteError: action.error };
          }

          return user;
        })
      };
    default:
      return state;
  }
}