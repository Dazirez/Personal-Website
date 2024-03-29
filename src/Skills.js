import React, { Component } from 'react';
import styled from 'styled-components';

const Flexbox = styled.div`
  margin: 100px 0px;
  display: flex;
  flex-direction: row;
  justify-content: center;
  flex-wrap: wrap;
  font-family: Apercu, 'Source Sans Pro', system, system-ui, -apple-system,
    BlinkMacSystemFont, Roboto, Helvetica, Arial, sans-serif;
`;
const Col = styled.div`
  margin-bottom: 25px;
  display: flex;
  flex-direction: column;
  margin-left: 75px;
`;

const UList = styled.ul`
  list-style: none;
`;
const Item = styled.li`
  font-size: 1.4em;
  display: list-item;
  padding: 5px;
`;
const Section = styled.li`
  font-size: 1.4em;
  font-weight: bold;
  padding: 5px;
`;
export class Skills extends Component {
  render() {
    return (
      <Flexbox id="projects'">
        <h4 style={{ color: '#007bff' }}> Skills </h4>
        <Col>
          <UList style={{ listStyle: 'none' }}>
            <Section>Languages</Section>
            <Item>JavaScript (ES6)</Item>
            <Item>C++</Item>
            <Item>C#</Item>
            <Item>Python</Item>
            <Item>SQL</Item>
            <Item>HTML/CSS</Item>
          </UList>
        </Col>
        <Col>
          <UList style={{ listStyle: 'none' }}>
            <Section>Frameworks</Section>
            <Item>React</Item>
            <Item>Flask</Item>
            <Item>Rest</Item>
            <Item>MongoDB</Item>
            <Item>NodeJS</Item>

          </UList>
        </Col>
        <Col>
          <UList style={{ listStyle: 'none' }}>
            <Section>Tools</Section>
            <Item>Unity</Item>
            <Item>VS Code</Item>
            <Item>Android Studio</Item>
            <Item>Git</Item>
            <Item>DaVinci Resolve</Item>
            <Item>Photoshop/Lightroom</Item>
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
      </Flexbox>
    );
  }
}

export default Skills;
