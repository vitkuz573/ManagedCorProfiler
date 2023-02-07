EXTERN EnterStub:PROC
EXTERN LeaveStub:PROC
EXTERN TailcallStub:PROC

_text SEGMENT PARA 'CODE'

ALIGN 16
PUBLIC EnterNaked3WithInfo

EnterNaked3WithInfo PROC FRAME

    PUSH RAX
    .PUSHREG RAX
    PUSH RCX
    .PUSHREG RCX
    PUSH RDX
    .PUSHREG RDX
    PUSH R8
    .PUSHREG R8
    PUSH R9
    .PUSHREG R9
    PUSH R10
    .PUSHREG R10
    PUSH R11
    .PUSHREG R11

    SUB RSP, 20H
    .ALLOCSTACK 20H

    .ENDPROLOG

    CALL EnterStub

    ADD RSP, 20H

    POP R11
    POP R10
    POP R9
    POP R8
    POP RDX
    POP RCX
    POP RAX

    RET

EnterNaked3WithInfo ENDP

ALIGN 16
PUBLIC LeaveNaked3WithInfo

LeaveNaked3WithInfo PROC FRAME

    PUSH RAX
    .PUSHREG RAX
    PUSH RCX
    .PUSHREG RCX
    PUSH RDX
    .PUSHREG RDX
    PUSH R8
    .PUSHREG R8
    PUSH R9
    .PUSHREG R9
    PUSH R10
    .PUSHREG R10
    PUSH R11
    .PUSHREG R11

    SUB RSP, 20H
    .ALLOCSTACK 20H

    .ENDPROLOG

    CALL LeaveStub

    ADD RSP, 20H

    POP R11
    POP R10
    POP R9
    POP R8
    POP RDX
    POP RCX
    POP RAX

    RET

LeaveNaked3WithInfo ENDP

ALIGN 16
PUBLIC TailcallNaked3WithInfo

TailcallNaked3WithInfo PROC FRAME

    PUSH RAX
    .PUSHREG RAX
    PUSH RCX
    .PUSHREG RCX
    PUSH RDX
    .PUSHREG RDX
    PUSH R8
    .PUSHREG R8
    PUSH R9
    .PUSHREG R9
    PUSH R10
    .PUSHREG R10
    PUSH R11
    .PUSHREG R11

    SUB RSP, 20H
    .ALLOCSTACK 20H

    .ENDPROLOG

    CALL TailcallStub

    ADD RSP, 20H

    POP R11
    POP R10
    POP R9
    POP R8
    POP RDX
    POP RCX
    POP RAX

    RET

TailcallNaked3WithInfo ENDP

_text ENDS

END