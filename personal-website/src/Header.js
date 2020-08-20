import React, { Component } from "react";
import "./assets/css/Header.css";

export class Header extends Component {
    render() {
        return (
            <div class="header">
                <ul>
                    <li>
                        <a href="#home"></a>Home
                    </li>
                    <li>
                        <a href="#about"></a>About
                    </li>
                    <li>
                        <a href="#contact"></a>Contact
                    </li>
                    <li>
                        <a href="#stream"></a>Stream
                    </li>
                </ul>
            </div>
        );
    }
}

export default Header;
