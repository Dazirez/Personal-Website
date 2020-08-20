import React, { Component } from "react";
import styled from "styled-components";



const Introduction = styled.header`
    width: 100%;
    padding: 120px 100px; 
    display: flex; 
    justify-content: space-around; 
    height: 100vh;
    font-family: Apercu,"Source Sans Pro",system,system-ui,-apple-system,BlinkMacSystemFont,Roboto,Helvetica,Arial,sans-serif;
`;
const Hello = styled.h1`
    display: block;
    padding: 120px 400px; 
`;
const Description = styled.h2`
    display: inline-block;
    width: 30%;
    padding-top: 120px;
    padding-bottom: 120px;
    padding-right: 100px;
    padding-left: 400px; 
`;
const Contact = styled.h3`
    display: block;
    padding: 120px 400px; 
`;
const Text = styled.body`
    font-family: Apercu,"Source Sans Pro",system,system-ui,-apple-system,BlinkMacSystemFont,Roboto,Helvetica,Arial,sans-serif;
    line-height: 1.5;
`;
const Propic = styled.img`
    width: 400px; 
    height: 400px;
    margin-left: 200px; 
`;
const Flexbox = styled.div`
    display: flex; 
    align-items: center; 
`;
const Email = styled.a`
    text-decoration: none;
    &:hover { 
        background-color: red;
        transform: scale(1.06)
    }
`;

export class Intro extends Component {
    render() {
        return (
            <Introduction>
                <Text>
                    <Hello>Hello!</Hello>
                    <Flexbox>
                        <Description>I'm Daniel, a Prospective Software Developer from Michigan. I’m currently studying computer science at the University of Michigan- Ann Arbor.

                        I love making music and experiencing new things. I play the guitar and piano, and am seriously curious about anything that comes my way.

                    If you’d like to make something happen, feel free to get in touch with me 🙂</Description>
                        <Propic src={require("./assets/img/Propic.png")} />
                    </Flexbox>

                    <Contact>Get in Touch <Email>Dbzheng@umich.edu</Email></Contact>
                </Text>
            </Introduction>
        );
    }
}

export default Intro;
