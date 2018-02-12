import React, { Component } from 'react'
import { connect } from 'react-redux'
import { Link } from 'react-router-dom';

class SearchForm extends Component {

    constructor(props) {
        super(props);

        this.state = {
            searchText: '',
            disabledButton: 'disabled'
        };
    }

    handleChange = (event) => {
        const { name, value } = event.target;
        const { searchText } = this.state;
        this.setState({
            searchText: value,
            disabledButton: (value.length > 0 ? '' : 'disabled')
        });

    }

    handleSubmit = (event) => {
        event.preventDefault();

        const { searchText } = this.state;
        if (searchText !== null & searchText.length > 0) {
            this.props.dispatch(this.props.customerAction(searchText));
        }
    }

    clearSearch = (event) => {
        event.preventDefault();
        this.setState({
            searchText: '',
            disabledButton: 'disabled'
        });
        this.props.dispatch(this.props.customerAction(''));
    }

    render() {

        const { searchText, disabledButton } = this.state

        return (
            <form name="form" className="form-inline" onSubmit={this.handleSubmit}>
                <label className="sr-only" htmlFor="searchText">Search</label>
                <input type="text" maxLength="20" className="form-control mb-4 mr-sm-4"
                    id="searchText" name="searchText" placeholder="Search customers"
                    value={searchText || ''} onChange={this.handleChange}
                />
                <button className="btn btn-primary mb-4 mr-sm-2" disabled={disabledButton}>Search</button>
                <Link to="/" className="btn btn-secondary mb-4" disabled={disabledButton} onClick={this.clearSearch}>
                    Clear Search
                </Link>
            </form>
        )
    }
}

export default connect()(SearchForm)