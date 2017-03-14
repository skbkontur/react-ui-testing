import React from 'react';
import Button from 'retail-ui/components/Button'
import Input from 'retail-ui/components/Input'
import { CaseSuite, Case } from '../Case';

export default class InputTextPage extends React.Component {
    state = {
        case1State: 1,
        case2State: 1,
        case3State: 1,
        case4State: 1,
        case5State: 1,
        case6State: 1,
    };

    render(): React.Element<*> {
        return (
            <CaseSuite title='Механизм переноса props-ов в DOM'>
                <Case title='Разные элементы переключаются между одним дом элементом' data-tid='SameDomElementCase'>
                    <Case.Body>
                        {this.state.case1State == 1
                            ? (
                                <Container data-tid='State1' title='Состояние 1'>
                                    Контент 1
                                </Container>
                            )
                            : (
                                <Container data-tid='State2' title='Состояние 2'>
                                    Контент 2
                                </Container>
                            )
                        }
                        <Button
                            data-tid='SwitchState'
                            onClick={() => this.setState({ case1State: this.state.case1State == 1 ? 2 : 1 })}>
                            Переключить состояние
                        </Button>
                    </Case.Body>
                </Case>


           </CaseSuite>
        );
    }
}

function DoubleNestingContainer({ state }) {
    return <NestingContainer state={state} />
}

function NestingContainer({ state }) {
    if (state === 1) {
        return <NestedComp1 />
    }
    return <NestedComp2 />
}

function NestedComp1() {
    return <div>Вложение 1</div>
}

function NestedComp2() {
    return <span>Вложение 2</span>
}

function NestingContainerOfDomElement({ state }) {
    if (state === 1) {
        return <div>Вложение 1</div>
    }
    return <span>Вложение 2</span>
}

function Container({ children, title }) {
    return (
        <div>
            <h3>{title}</h3>
            <div>
                {children}
            </div>
        </div>
    )
}
