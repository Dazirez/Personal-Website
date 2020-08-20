import React, { Component } from "react";
import styled from "styled-components";

const Flexbox = styled.div`
    display: flex; 
    margin-bottom: 400px;
    flex-direction: row;
    justify-content: center; 
    font-family: Apercu,"Source Sans Pro",system,system-ui,-apple-system,BlinkMacSystemFont,Roboto,Helvetica,Arial,sans-serif;
`;
const Col = styled.div`
    margin-bottom: 25px; 
    display: flex; 
    flex-direction: column; 
    margin-left: 100px; 

`;

const Content = styled.div`
    justify-content: space-around; 
    margin-left: 100px; 
    display: flex; 
    max-width: 30%;
    flex-direction: row;
    justify-content: center; 
`;

const UList = styled.ul`
    list-style: none;
    margin: 3px 0px; 
`;
const Item = styled.li`
    display: list-item;
    padding: 5px;
`;
const Section = styled.li`
    font-weight: bold; 
    padding: 5px;

`;
export class Skills extends Component {
    render() {
        return (
            <Flexbox>
                <h4 style={{ color: '#007bff' }}> Skills </h4>
                <Content>
                    <Col>
                        <UList style={{ listStyle: 'none' }}>
                            <Section>Languages</Section>
                            <Item>JavaScript (ES6)</Item>
                            <Item>C++</Item>
                            <Item>Python</Item>
                            <Item>HTML</Item>
                            <Item>CSS</Item>
                        </UList>
                    </Col>
                    <Col>
                        <UList style={{ listStyle: 'none' }}>
                            <Section>Frameworks</Section>

                            <Item>React</Item>
                            <Item>Flask</Item>
                        </UList>
                    </Col>
                    <Col>
                        <UList style={{ listStyle: 'none' }}>
                            <Section>Tools</Section>

                            <Item>Photoshop</Item>
                            <Item>Git</Item>
                            <Item>Final Cut Pro</Item>
                            <Item>Piskel</Item>
                            <Item>Clip Studio Paint</Item>
                        </UList>
                    </Col>
                    <Col>
                        <UList style={{ listStyle: 'none' }}>
                            <Section>Design</Section>

                            <Item>InDesign</Item>
                            <Item>Invision</Item>
                            <Item>Figma</Item>
                            <Item>WireFraming</Item>
                            <Item>User Testing</Item>

                        </UList>
                    </Col>

                </Content>
            </Flexbox>
        );
    }
}

export default Skills;
