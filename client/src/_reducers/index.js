import { combineReducers } from 'redux';

import { customers } from './customers.reducer';
import { alert } from './alert.reducer';

const rootReducer = combineReducers({
  customers,
  alert
});

export default rootReducer;