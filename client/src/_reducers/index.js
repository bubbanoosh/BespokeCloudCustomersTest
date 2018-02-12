import { combineReducers } from 'redux';

import { customersState } from './customers.reducer';
import { alert } from './alert.reducer';

const rootReducer = combineReducers({
    customersState,
  alert
});

export default rootReducer;