import React from 'react';
import { appConfig } from '../../../_helpers/'

export const Header = (props) => {
    return (
        <div>
            <div className="jumbotron">
                <h1>{appConfig.APP_HEADING}</h1>
            </div>
        </div>
    );
}
