import React from 'react';
import { render } from 'react-dom';
import { Provider } from 'react-redux';
import { ConnectedRouter } from 'react-router-redux';

import { store } from './_store/store';
import { history } from './_helpers/';

import './index.css';
import { App } from './App/';
import registerServiceWorker from './registerServiceWorker';

const target = document.querySelector('#root');

// Renamed to index.js (https://github.com/facebook/create-react-app/issues/3052)

render(
    <Provider store={store}>
        <ConnectedRouter history={history}>
            <App />
        </ConnectedRouter>
    </Provider>,
    target
);
registerServiceWorker();
