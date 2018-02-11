import React from 'react';
import { render } from 'react-dom';
import { Provider } from 'react-redux';
import { ConnectedRouter } from 'react-router-redux'

import { store } from './_store/store';
import { history } from './_helpers/';

import './index.css';
import { App } from './App/';
import registerServiceWorker from './registerServiceWorker';

const target = document.querySelector('#root')

render(
  <Provider store={store}>
    <ConnectedRouter history={history}>
      <div>
        <App />
      </div>
    </ConnectedRouter>
  </Provider>,
  target
)
registerServiceWorker();
