import 'babel-polyfill';
import 'raf/polyfill';
import React from 'react';
import ReactDOM from 'react-dom';
import App from '../../src/App';

import configureStore from 'redux-mock-store'
 
// create any initial state needed
const initialState = {}; 
// here it is possible to pass in any middleware if needed into //configureStore
const mockStore = configureStore();
let wrapper;
let store;
beforeEach(() => {
  //creates the store with any initial state or middleware needed  
  store = mockStore(initialState)
  wrapper = shallow(<App store={store}/>)
 })


it('renders without crashing', () => {
  const div = document.createElement('div');
  ReactDOM.render(<App />, div);
  ReactDOM.unmountComponentAtNode(div);
});
