import React, { Component } from "react";
import styled from "styled-components";
const introduction = styled.section``;

export class Intro extends Component {
    render() {
        return (
            <introduction>
                <h1>Hello</h1>
                <h2>I'm Daniel Zheng. A Developer, Musician, and Adventurer</h2>
                <h3>Get in Touch</h3>
            </introduction>
        );
    }
}

export default Intro;
