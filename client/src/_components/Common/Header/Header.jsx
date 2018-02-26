import React from 'react';
import { appConfig } from '../../../_helpers/';

const Fragment = React.Fragment;

export const Header = () => {
    return (
        <Fragment>
            <div className="jumbotron">
                <h1>{appConfig.APP_HEADING}</h1>
            </div>
        </Fragment>
    );
};
