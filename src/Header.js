import React, { Component } from "react";
import "./assets/css/Header.css";

export class Header extends Component {
    render() {
        return (
            <div class="header">
                <ul>
                    <li>
                        <a href="#"></a>Home
                    </li>
                    <li>
                        <a href="#"></a>About
                    </li>
                    <li>
                        <a href="#"></a>Contact
                    </li>
                    <li>
                        <a href="#"></a>Stream
                    </li>
                </ul>
            </div>
        );
    }
}

export default Header;
