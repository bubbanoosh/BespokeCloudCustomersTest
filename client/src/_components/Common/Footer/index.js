import React from 'react';
import { appConfig } from '../../../_helpers/'

const styles = {
    footer: {
        /* Set the fixed height of the footer here */
        height: '60px',
        backgroundColor: '#f5f5f5',
        paddingTop: '1rem'
    }
}

export const Footer = (props) => {
    return (
        <div className="container">
            <div class="row">
                <div class="col-md-12">
                    <footer className="footer" style={styles.footer}>
                        <div className="container">
                            <p className="text-muted">{appConfig.APP_HEADING}</p>
                        </div>
                    </footer>
                </div>
            </div>
        </div>
    );
}
