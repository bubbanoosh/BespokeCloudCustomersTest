import React from 'react';
import { appConfig } from '../../../_helpers/'

const styles = {
    footer: {
        position: 'absolute',
        bottom: 0,
        width: '100%',
        /* Set the fixed height of the footer here */
        height: '60px',
        backgroundColor: '#f5f5f5',
        paddingTop: '1rem'
    }
}

export const Footer = (props) => {
    return (
        <footer className="footer" style={styles.footer}>
            <div className="container">
                <p className="text-muted">{appConfig.APP_HEADING}</p>
            </div>
        </footer>
    );
}
