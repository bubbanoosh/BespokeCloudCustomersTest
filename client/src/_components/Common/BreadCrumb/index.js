import React, { Component } from 'react'

class BreadCrumb extends Component {

    getCrumbs = (links, text, active) => {
        return links.map(l => {
            return `<li className="breadcrumb-item">${<Link to="/">Home</Link>}</li>`
        })
    }
// ES6 maps
//https://hackernoon.com/what-you-should-know-about-es6-maps-dc66af6b9a1e

    render() {

        const { links = [], text = [], active = '' } = this.props

        return (
            <nav aria-label="breadcrumb">
                <ol className="breadcrumb">
                    <li className="breadcrumb-item"><Link to="/">Home</Link></li>
                    <li className="breadcrumb-item active" aria-current="page">Add New Customer</li>
                </ol>
            </nav>
        )
    }
}

export default BreadCrumb